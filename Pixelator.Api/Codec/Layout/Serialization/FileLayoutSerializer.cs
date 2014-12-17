using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pixelator.Api.Codec.Structures;
using Directory = Pixelator.Api.Output.Directory;
using File = Pixelator.Api.Output.File;

namespace Pixelator.Api.Codec.Layout.Serialization
{
    internal sealed class FileLayoutSerializer : Serializer<FileLayout>
    {
        protected override Task SerializeEntity(BinaryWriter writer, FileLayout entity)
        {
            writer.Write(entity.Directories.Count);
            foreach (Directory directory in entity.Directories)
            {
                writer.Write(directory.Path);

                writer.Write(directory.Files.Count);
                foreach (File file in directory.Files)
                {
                    writer.Write(file.Name);
                    writer.Write(file.Length);
                    writer.Write(file.Guid.ToByteArray());
                }
            }

            writer.Write(entity.OrderdFileGroups.Count);
            foreach (FileGroup group in entity.OrderdFileGroups)
            {
                writer.Write(group.Files.Count);
                foreach (File file in group.Files)
                {
                    writer.Write(file.Guid.ToByteArray());
                }
            }

            return Task.FromResult(0);
        }

        protected override Task<FileLayout> DeserializeBytesAsync(BinaryReader reader)
        {
            var directories = new Directory[reader.ReadInt32()];

            for (int iDirectory = 0; iDirectory < directories.Length; iDirectory++)
            {
                string path = reader.ReadString();

                var files = new File[reader.ReadInt32()];
                for (int iFile = 0; iFile < files.Length; iFile++)
                {
                    files[iFile] = (new File(reader.ReadString(), reader.ReadInt64(), new Guid(reader.ReadBytes(16))));
                }

                directories[iDirectory] = new Directory(path, files);
            }

            List<File> allFiles = directories.SelectMany(directory => directory.Files).ToList();
            var fileGroups = new FileGroup[reader.ReadInt32()];

            for (int iFileGroup = 0; iFileGroup < fileGroups.Length; iFileGroup++)
            {
                var files = new File[reader.ReadInt32()];
                for (int iFile = 0; iFile < files.Length; iFile++)
                {
                    var guid = new Guid(reader.ReadBytes(16));
                    files[iFile] = allFiles.Single(file => file.Guid == guid);
                }

                fileGroups[iFileGroup] = (new FileGroup(files));
            }

            return Task.FromResult(new FileLayout(directories, fileGroups));
        }
    }
}