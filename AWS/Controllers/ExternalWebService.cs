using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AWS.Controllers
{
    public class ExternalWebService : Controller
    {
        /*
         @author 121220076 Muhammed Eminoğlu
         */

        //Web Servisi Sistemde multi thread şekilde çalışabilmesi için...
        #region Delegates
        public delegate string DelegateInvokeService();
        #endregion

        // Servis tipini belirtmek için enumarator
        #region Enumerators
        public enum ServiceType
        {
            Traditional = 0,
            WCF = 1
        }
        #endregion

        //Web Servise gönderilecek parametler için oluşturulmuş class
        #region Classes
        public class Parameter
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
        #endregion

        //Web Servis için header bilgisi
        #region Member Variables
        string _soapEnvelope =
                @"<soap:Envelope
                    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
                    xmlns:xsd='http://www.w3.org/2001/XMLSchema'
                    xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>
                <soap:Body></soap:Body></soap:Envelope>";

        #endregion

        #region Properties
        //Web Servis URL
        public string Url { get; set; }
        //Web Servis Methodu
        public string WebMethod { get; set; }
        //Parametreler için parametre listesi
        public List<Parameter> Parameters { get; set; }
        // Web servis Enumator nesnesi
        public ServiceType WSServiceType { get; set; }
        //Web servis contract name bilgisi...
        public string WCFContractName { get; set; }
        #endregion

        //Web Servis Ekleyen Controller
        public ActionResult ExternalWebServiceEkle()
        {
            return View();
        }

        // Web servis header bilgilerini ve parametrelerini canlı olarak oluşturan fonksiyon.
        #region Private Methods
        private string CreateSoapEnvelope()
        {
            string MethodCall = "<" + this.WebMethod + @" xmlns=""http://tempuri.org/"">";
            string StrParameters = string.Empty;

            foreach (Parameter param in this.Parameters)
            {
                StrParameters = StrParameters + "<" + param.Name + ">" + param.Value + "</" + param.Name + ">";
            }

            MethodCall = MethodCall + StrParameters + "</" + this.WebMethod + ">";

            StringBuilder sb = new StringBuilder(_soapEnvelope);
            sb.Insert(sb.ToString().IndexOf("</soap:Body>"), MethodCall);

            return sb.ToString();
        }

        // Web Servis Sorgusunu oluşturan kod...
        private HttpWebRequest CreateWebRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(this.Url);
            if (this.WSServiceType == ServiceType.WCF)
                webRequest.Headers.Add("SOAPAction", "\"http://tempuri.org/" + this.WCFContractName + "/" + this.WebMethod + "\"");
            else
                webRequest.Headers.Add("SOAPAction", "\"http://tempuri.org/" + this.WebMethod + "\"");

            webRequest.Headers.Add("To", this.Url);

            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        //Dönen cevabı parse eden fonksiyon.
        private string StripResponse(string SoapResponse)
        {
            string RegexExtract = @"<" + this.WebMethod + "Result>(?<Result>.*?)</" + this.WebMethod + "Result>";

            return Regex.Match(SoapResponse, RegexExtract).Groups["Result"].Captures[0].Value;
        }
        #endregion

        #region Public Methods
        //Delegate oluşturan fonksiyon. Multithread için...
        public void BeginInvokeService(AsyncCallback InvokeCompleted)
        {
            DelegateInvokeService Invoke = new DelegateInvokeService(this.InvokeService);

            IAsyncResult result = Invoke.BeginInvoke(InvokeCompleted, null);
        }

        //Thread'î sonlandıran fonksiyon...
        public string EndInvokeService(IAsyncResult result)
        {
            var asyncResult = (AsyncResult)result;
            ReturnMessage msg = (ReturnMessage)asyncResult.GetReplyMessage();

            return msg.ReturnValue.ToString();
        }

        // Web servisi çağıran fonksiyon.
        public string InvokeService()
        {
            WebResponse response = null;
            string strResponse = "";
            //Sorguyu oluşturuyoruz.
            HttpWebRequest req = this.CreateWebRequest();
            //write the soap envelope to request stream
            using (Stream stm = req.GetRequestStream())
            {
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(this.CreateSoapEnvelope());
                }
           }
            //get the response from the web service
            response = req.GetResponse();
            Stream str = response.GetResponseStream();
            StreamReader sr = new StreamReader(str);
            strResponse = sr.ReadToEnd();
            return this.StripResponse(HttpUtility.HtmlDecode(strResponse));
        }
        #endregion
    }
}