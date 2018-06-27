using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BSK_Projekt2.Models
{
    public class EntityClearance
    {

        public EntityClearance()
        {
            Classification = new Dictionary<string, int>();
        }
        public EntityClearance(int create, int read, int update, int delete)
        {
            Classification = new Dictionary<string, int>
            {
                { "create", create },
                { "read", read },
                { "update", update },
                { "delete", delete }
            };

        }
        public string EntityName { get; set; }
        public Dictionary<string, int> Classification { get; set; }


    }
}