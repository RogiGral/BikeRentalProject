﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Security
{
    public class PasswordEncryption
    {
        public string Encode(string password)
        {
            try
            {
                byte[] EncDataByte = new byte[password.Length];
                EncDataByte = System.Text.Encoding.UTF8.GetBytes(password);
                string EncryptedData = Convert.ToBase64String(EncDataByte);
                return EncryptedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in encode" + ex.Message);
            }
        }
    }
}