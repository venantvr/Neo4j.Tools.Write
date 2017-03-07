﻿using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using Neo4j.Tools.Write.Interfaces;

namespace Neo4j.Tools.Write.Hash
{
    public class Md5HashProcessor : IHashProcessor
    {
        public string GetHash<T>(T instance)
        {
            var cryptoServiceProvider = new MD5CryptoServiceProvider();
            return ComputeHash(instance, cryptoServiceProvider);
        }

        private string ComputeHash(object instance, HashAlgorithm cryptoServiceProvider)
        {
            var serializer = new DataContractSerializer(instance.GetType());
            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, instance);
                cryptoServiceProvider.ComputeHash(memoryStream.ToArray());
                return Convert.ToBase64String(cryptoServiceProvider.Hash);
            }
        }
    }
}