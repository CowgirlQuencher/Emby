﻿using System;
using System.Collections.Generic;
using MediaBrowser.Model.IO;
using MediaBrowser.Model.Serialization;

namespace MediaBrowser.Controller.Entities
{
    public class LinkedChild
    {
        public string Path { get; set; }
        public LinkedChildType Type { get; set; }

        [IgnoreDataMember]
        public string Id { get; set; }

        /// <summary>
        /// Serves as a cache
        /// </summary>
        public Guid? ItemId { get; set; }

        public static LinkedChild Create(BaseItem item)
        {
            return new LinkedChild
            {
                Path = item.Path,
                Type = LinkedChildType.Manual
            };
        }

        public LinkedChild()
        {
            Id = Guid.NewGuid().ToString("N");
        }
    }

    public enum LinkedChildType
    {
        Manual = 0,
        Shortcut = 1
    }

    public class LinkedChildComparer : IEqualityComparer<LinkedChild>
    {
        private readonly IFileSystem _fileSystem;

        public LinkedChildComparer(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public bool Equals(LinkedChild x, LinkedChild y)
        {
            if (x.Type == y.Type)
            {
                return _fileSystem.AreEqual(x.Path, y.Path);
            }
            return false;
        }

        public int GetHashCode(LinkedChild obj)
        {
            return (obj.Path + obj.Type).GetHashCode();
        }
    }
}
