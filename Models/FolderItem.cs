using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StringLocalizer.Models.ClassItem;

namespace StringLocalizer.Models
{
    internal class FolderItem
    {
        public FolderItem(string name, FolderItem parent)
        {
            Name = name;
            Parent = parent;
        }
        public string Name { get; private set; }
        public FolderItem Parent { get; set; }
        public FolderItem[] SubFolders { get; private set; } = [];
        public ClassItem[] ClassItems { get; private set; } = [];

        private bool _hasLocalizers;
        public bool HasLocalizers 
        { 
            get => _hasLocalizers;
        }
        public void AddClassItem(string name, string extension, IEnumerable<string> keys)
        {
            ItemTypeEnum itemType;
            switch (extension.ToLower())
            {
                case ".cs":
                    itemType = ItemTypeEnum.CSharp;
                    break;
                case ".cshtml":
                    itemType = ItemTypeEnum.CSHTML;
                    break;
                case ".razor":
                    itemType = ItemTypeEnum.Razor;
                    break;
                default:
                    itemType = ItemTypeEnum.CSharp;
                    break;
            }
            ClassItems = ClassItems.Append(new ClassItem(name, itemType, keys, this)).ToArray();
            SetHasLocalizers();
        }
        public void AddClassCSItem(string name, IEnumerable<string> keys)
        {
            ClassItems = ClassItems.Append(new ClassItem(name, ItemTypeEnum.CSharp, keys, this)).ToArray();
            SetHasLocalizers();
        }
        public void SetHasLocalizers()
        {
            _hasLocalizers = true;
            if (Parent != null)
                Parent.SetHasLocalizers();
        }

        public FolderItem AddSubFolder(string name)
        {
            var folderItem = new FolderItem(name, this);
            SubFolders = SubFolders.Append(folderItem).ToArray();
            return folderItem;
        }

        public string GetFullPath()
        {
            if (Parent == null)
                return Name;
            return Path.Combine(Parent.GetFullPath(), Name);
        }
    }

    internal class ClassItem
    {
        public enum ItemTypeEnum
        {
            CSharp,
            CSHTML,
            Razor
        }

        public ClassItem(string name, ItemTypeEnum itemType, IEnumerable<string> keys, FolderItem parent)
        {
            Name = name;
            ItemType = itemType;
            Keys = keys.ToArray();
            Parent = parent;
        }

        public string Name { get; private set; }
        public ItemTypeEnum ItemType { get; private set; }
        public FolderItem Parent { get; set; }
        public string[] Keys { get; private set; } = [];
        public string GetFullPath()
        {
            return Parent.GetFullPath();
        }
    }
}
