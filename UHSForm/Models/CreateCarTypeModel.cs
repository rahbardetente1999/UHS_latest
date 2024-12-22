﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class CreateCarTypeModel
    {
        public string Name { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<int> rID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class UpdateCarTypeModel
    {
        public string Name { get; set; }
        public Nullable<int> ID { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class DeleteCarTypeModel
    {
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> ID { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class GetCarTypeModel
    {
        public string Name { get; set; }
        public Nullable<int> ID { get; set; }
        public Nullable<DateTime> AddedOn { get; set; }
        public string AddedBy { get; set; }
    }
}