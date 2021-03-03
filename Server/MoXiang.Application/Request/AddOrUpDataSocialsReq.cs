using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Application.Request
{
    public class AddOrUpDataSocialsReq
    {

        public int UserInfoId { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public string Color { get; set; }

        public string Href { get; set; }
    }
}
