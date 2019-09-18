using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExplorerTree
{
    public abstract class SystemComponent
    {
        public string Name { get; }

        public string Path { get; }

        public DateTime Created { get; }

        public SystemComponent(string name, string path, DateTime created)
        {
            Name = name;
            Path = path;
            Created = created;
        }

        public override string ToString()
        {
            return Path;
        }
    }

    public class FileItem : SystemComponent
    {
        public FileItem(string name, string path, DateTime created, long size)
            : base(name, path, created)
        {
            Size = (int)size / 1024;
        }

        public int Size { get; }
    }

    public class FolderItem : SystemComponent
    {
        public FolderItem(string name, string path, DateTime created)
            : base(name, path, created)
        {
        }
    }

    public class TreeNode<T>
    {
        public T Data { get; set; }

        public ICollection<TreeNode<T>> Children { get; private set; }

        public TreeNode(T data)
        {
            this.Data = data;

        }

        public TreeNode<T> AddChild(T child)
        {
            if (Children == null)
            {
                this.Children = new List<TreeNode<T>>();
            }

            TreeNode<T> childNode = new TreeNode<T>(child);
            Children.Add(childNode);
            return childNode;
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }

    public static class Explorer
    {
        public static TreeNode<SystemComponent> GetGraph(string root, int width, int depth)
        {
            if (!Directory.Exists(root))
            {
                throw new DirectoryNotFoundException($"Directory \"{root}\" cannot be found!");
            }

            try
            {
                var rootInfo = new DirectoryInfo(root);
                var folderItem = new FolderItem(rootInfo.Name, rootInfo.FullName, rootInfo.CreationTime);
                TreeNode<SystemComponent> rootComponent = new TreeNode<SystemComponent>(folderItem);
                ProcessNode(rootComponent, width, depth);
                return rootComponent;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Ошибка обработки каталога " + root);
            }

        }

        public static TreeNode<string> GetTree(string rootPath)
        {
            var catalogs = new Stack<TreeNode<string>>();
            var root = new TreeNode<string>(rootPath);
            catalogs.Push(root);
            while (catalogs.Count != 0)
            {
                var currentNode = catalogs.Pop();
                string[] subCatalogs = null;
                try
                {
                    subCatalogs = Directory.GetDirectories(currentNode.Data);
                }
                catch (Exception)
                {
                    // TODO: Add logging
                    continue;
                }

                foreach (var catalog in subCatalogs)
                {
                    var child = currentNode.AddChild(catalog);
                    catalogs.Push(child);
                }
            }

            return root;
        }

        private static void ProcessNode(TreeNode<SystemComponent> currentItem, int width, int maxDepth)
        {
            if (maxDepth == 0)
            {
                return;
            }

            IEnumerable<FileSystemInfo> directoryItems = null;
            try
            {
                var directoryInfo = new DirectoryInfo(currentItem.Data.Path);
                directoryItems = directoryInfo.EnumerateFileSystemInfos("*", SearchOption.TopDirectoryOnly).Take(width).ToList();
            }
            catch (Exception)
            {
                // TODO: Add logging
                return;
            }

            foreach (var item in directoryItems)
            {
                SystemComponent treeComponent = null;
                if (item.Attributes.HasFlag(FileAttributes.Directory))
                {
                    treeComponent = new FolderItem(item.Name, item.FullName, item.CreationTime);
                    var folderNode = currentItem.AddChild(treeComponent);
                    ProcessNode(folderNode, width, maxDepth - 1);
                }
                else
                {
                    treeComponent = new FileItem(item.Name, item.FullName, item.CreationTime, (item as FileInfo).Length);
                    currentItem.AddChild(treeComponent);
                }
            }
        }
    }
}
