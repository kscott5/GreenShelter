using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Xunit;

using PCSC.GreenShelter.Controllers;

namespace PCSC.GreenShelter.Test.Controllers
{
    public class SpaControllerTest
    {
        [Fact]
        public void TagName() {
            var actual = new SpaController();
            
            Assert.Equal("SpaController", actual.TagName);
        }
        
        [Fact]
        public void StartPage() {
            var ctrl = new SpaController();
            var actual = ctrl.StartPage();
            
            
        }
    }
}
