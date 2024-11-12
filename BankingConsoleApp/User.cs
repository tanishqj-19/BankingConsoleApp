﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }

    public List<Account> Accounts { get; set; } = new List<Account>();
}