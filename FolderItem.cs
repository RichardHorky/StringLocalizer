using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StringLocalizer.ClassItem;

namespace StringLocalizer
{
    internal class FolderItem
    {
        public FolderItem(string name)
        {
            Name = name;
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
            ClassItems = ClassItems.Append(new ClassItem(name, itemType, keys)).ToArray();
            SetHasLocalizers();
        }
        public void AddClassCSItem(string name, IEnumerable<string> keys)
        {
            ClassItems = ClassItems.Append(new ClassItem(name, ItemTypeEnum.CSharp, keys)).ToArray();
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
            var folderItem = new FolderItem(name);
            folderItem.Parent = this;
            SubFolders = SubFolders.Append(folderItem).ToArray();
            return folderItem;
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

        public ClassItem(string name, ItemTypeEnum itemType, IEnumerable<string> keys)
        {
            Name = name;
            ItemType = itemType;
            Keys = keys.ToArray();
        }

        public string Name { get; private set; }
        public ItemTypeEnum ItemType { get; private set; }
        public FolderItem Parent { get; set; }
        public string[] Keys { get; private set; } = [];
    }
}
