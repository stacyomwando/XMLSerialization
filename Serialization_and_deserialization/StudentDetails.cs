using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Serialization_and_deserialization
{
    [XmlRoot("Student")]
    public class StudentDetails
    {

		[XmlElement("AdmissionNumber")]
		public int AdmissionNumber { get; set; }

		[XmlElement("StudentName")]
		public string StudentName { get; set; }


		[XmlElement("Course")]
		public string Course { get; set; }

		[XmlElement("Year")]
		public int Year { get; set; }

		

	}
}
