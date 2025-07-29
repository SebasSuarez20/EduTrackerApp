using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EduTrackServer.CapaDatos.Model
{
    public class ResponseModel<T>
    {
        public HttpStatusCode Status { get; set; }
        public string? Message { get; set; }
        public T? DataContent { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; set; }
    }

    public class ResponseModelSp<T> {

        public T MetaData {get;set;}
     }
}
