using AiursoftBase.Models;
using AiursoftBase.Services;
using Developer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Developer.Models
{
    public class App
    {
        public App(string seed, string name, Category category, Platform platform)
        {
            this.AppId = (seed + DateTime.Now.ToString()).GetMD5();
            this.AppSecret = (seed + this.AppId + DateTime.Now.ToString() + StringOperation.RandomString(15)).GetMD5();
            this.AppName = name;
            this.AppCategory = category;
            this.AppPlatform = platform;
        }
        public virtual string AppId { get; set; }
        public virtual string AppSecret { get; set; }
        public virtual string AppName { get; set; }
        public virtual DateTime AppCreateTime { get; set; }
        public virtual Category AppCategory { get; set; }
        public virtual Platform AppPlatform { get; set; }

        public virtual string CreaterId { get; set; }
        [ForeignKey(nameof(CreaterId))]
        public virtual DeveloperUser Creater { get; set; }
    }
}
