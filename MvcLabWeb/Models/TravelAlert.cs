using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcLabWeb.Models
{
    public class TravelAlert
    {
        [Display(Name = "警示發送時間")]
        public DateTime sent { get; set; }

        [Display(Name = "來源簡述")]
        public string source { get; set; }

        [Display(Name = "有效日期與時間")]
        public DateTime effective { get; set; }

        [Display(Name = "到期日期與時間")]
        public DateTime expires { get; set; }

        [Display(Name = "發送者名稱")]
        public string senderName { get; set; }

        [Display(Name = "標題 ")]
        public string headline { get; set; }

        [Display(Name = "描述")]
        public string description { get; set; }

        [Display(Name = "描述建議採取應變方案")]
        public string instruction { get; set; }

        [Display(Name = "其他資訊連結")]
        public string web { get; set; }

        [Display(Name = "警示標題")]
        public string alert_title { get; set; }

        [Display(Name = "嚴重程度")]
        public string severity_level { get; set; }

        [Display(Name = "疾病")]
        public string alert_disease { get; set; }

        [Display(Name = "區域描述")]
        public string areaDesc { get; set; }

        [Display(Name = "英文區域描述")]
        public string areaDesc_EN { get; set; }

        [Display(Name = "中心點座標及半徑")]
        public string circle { get; set; }

        [Display(Name = "國家或地區標準代碼")]
        public string ISO3166 { get; set; }

        [Display(Name = "省市")]
        public object areaDetail { get; set; }

        [Display(Name = "為省市的代碼")]
        public object ISO3166_2 { get; set; }
    }
}


