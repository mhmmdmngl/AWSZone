using AWS.Filter;
using AWS.Models;
using AWS.Repos;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AWS.Controllers
{
    public class RestController : ApiController
    {
        wsLog log = new wsLog();
        wsLogCTX logCTX = new wsLogCTX();
        WebServisCTX wsCtx = new WebServisCTX();
        userCTX uctx = new userCTX();
        fonksiyonCTX fctx = new fonksiyonCTX();
        DBCTX dbctx = new DBCTX();
        wsUserCTX wuctx = new wsUserCTX();

        [WebApiAuth]
        [System.Web.Http.HttpGet]
        public dynamic MyService(string wsUser, string wsPassword, string webServisAdi, string fonksiyonAdi, int auto = 1)
        {
            long sorguBaslangici = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
           
            //var user = uctx.kullaniciDondur(wsUser);
            var user = wuctx.userDogrula(wsUser, wsPassword);
            log.userid = user.userId;
            if (user == null)
                return "Kullanıcı bulunamadı";

            log.wsuserid = user.id;

            restApiCTX ractx = new restApiCTX();
            restParametersCTX rparams = new restParametersCTX();
             
            var restApiBul = ractx.restApiDondurByKullanici(user.id, webServisAdi);
            if (restApiBul == null)
                return "ilgili rest servis bulunamadı";

            var parametreler = rparams.restParamDondurDondurbyRestId(restApiBul.id);
            string callAction = restApiBul.apiurl;
            foreach(var parametre in parametreler)
            {
                var gelenParam = HttpContext.Current.Request.Params[parametre.valueName.ToString()].ToString();
                callAction = callAction + parametre.valueName + "=" + gelenParam + "&";
                
            }
            callAction = callAction.TrimEnd('&');

            var client = new RestClient(callAction);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return response.Content;


        }

    }
}