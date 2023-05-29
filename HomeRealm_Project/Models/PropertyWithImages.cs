using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeRealm_Project.Models
{
    public class PropertyWithImagesModel
    {
        public Property Ourproperty { set; get; }
        public List<PropertyImage> images { set; get; }
    }
}