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
        /// <summary>
        /// Indicates whether this branch contains any class items containing localizable keys.
        /// </summary>
        public bool HasLocalizers 
        { 
            get => _hasLocalizers;
        }

        private bool _matchFilter;
        /// <summary>
        /// Indicates whether this branch contains any class items matching the current filter.
        /// </summary>
        public bool MatchFilter
        {
            get => _matchFilter;
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
        /// <summary>
        /// Sets the has localizers flag for this folder and all parents.
        /// </summary>
        public void SetHasLocalizers()
        {
            _hasLocalizers = true;
            if (Parent != null)
                Parent.SetHasLocalizers();
        }
        /// <summary>
        /// Sets the match filter flag for this folder and all parents.
        /// </summary>
        public void SetMatchFilter()
        {
            _matchFilter = true;
            if (Parent != null)
                Parent.SetMatchFilter();
        }
        /// <summary>
        /// Clears the match filter flag for this folder and all subfolders.
        /// </summary>
        public void ClearMatchFilter()
        {
            _matchFilter = false;
            foreach (var subFolder in SubFolders)
            {
                subFolder.ClearMatchFilter();
            }
        }

        public FolderItem AddSubFolder(string name)
        {
            var folderItem = new FolderItem(name, this);
            SubFolders = SubFolders.Append(folderItem).ToArray();
            return folderItem;
        }
        /// <summary>
        /// Returns the full path of the folder.
        /// </summary>
        /// <returns></returns>
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

        private bool _matchFilter;
        public bool MatchFilter
        {
            get => _matchFilter;
        }
        /// <summary>
        /// Returns the full path of the class item.
        /// </summary>
        /// <returns></returns>
        public string GetFullPath()
        {
            return Parent.GetFullPath();
        }

        public void SetMatchFilter()
        {
            _matchFilter = true;
            Parent.SetMatchFilter();
        }
    }
}
