using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance.Dtos.Student
{
    internal class StudentDto
    {
        public int Id { get; set; }

        [AllowedValues()]
        public string Name { get; set; } = null!;
    }

    public static class StudentConst
    {
        public const string Name1 = "Name1";
    }


}