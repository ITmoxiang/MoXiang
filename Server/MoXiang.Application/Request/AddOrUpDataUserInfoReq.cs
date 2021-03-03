using MoXiang.Application.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoXiang.Application.Response
{
    public class AddOrUpDataUserInfoReq
    {

        public string Account { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string EMail { get; set; }

        public string Phone { get; set; }

        public string Slogan { get; set; }

        public string Icon { get; set; }

        public string Notice { get; set; }

        public virtual List<AddOrUpDataSocialsReq> socials { get; set; }
    }
}
