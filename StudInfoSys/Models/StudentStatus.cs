using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace StudInfoSys.Models
{
    public enum StudentStatus
    {
        [Description("Preparatory - On Going")]
        PreparatoryOnGoing = 1,

        [Description("Preparatory - Finished")]
        PreparatoryFinished,

        [Description("Elementary - On Going")]
        ElementaryOnGoing,

        [Description("Elementary - Finished")]
        ElementaryFinished,

        [Description("High School - On Going")]
        HighSchoolOnGoing,

        [Description("High School - Finished")]
        HighSchoolFinished,

        [Description("Undergraduate Studies - On Going")]
        UndergraduateStudiesOnGoing,

        [Description("Undergraduate Studies - Finished")]
        UndergraduateStudiesFinished,

        [Description("Graduate Studies - On Going")]
        GraduateStudiesOnGoing,

        [Description("Graduate Studies - Finished")]
        GraduateStudiesFinished,

        [Description("Not A Student Anymore")]
        NotAStudentAnymore
    }
}
