﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchool.DataAccess.Entities
{
    public class AnswerUserTestEntity
    {
        public Guid Id { get; set; }
        public UserEntity? User { get; set; }
        public TestEntity? Test { get; set; }
        public int ResultTest { get; set; }
    }
}
