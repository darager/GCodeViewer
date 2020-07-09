using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using g3;

namespace GCodeViewer.Library
{
    public class STLFile
    {
        private string _filePath;

        public STLFile(string filePath)
        {
            _filePath = filePath;
        }

        public List<DMesh3> LoadMeshes()
        {
            using var stream = File.OpenRead(_filePath);
            using var binaryReader = new BinaryReader(stream);

            var builder = new DMesh3Builder();
            var reader = new STLReader();

            var result = reader.Read(binaryReader, ReadOptions.Defaults, builder);

            binaryReader.Close();
            stream.Close();

            if (result.code != IOCode.Ok)
                throw new Exception("Something went wrong when reading the STL file!");

            return builder.Meshes;
        }

        public void SaveMeshes(List<DMesh3> meshes)
        {
            using var stream = File.OpenWrite(_filePath);
            var binaryWriter = new BinaryWriter(stream);

            var writeMeshes = meshes.Select(m => new WriteMesh(m))
                                    .ToList();

            var writer = new STLWriter();
            writer.Write(binaryWriter, writeMeshes, WriteOptions.Defaults);

            binaryWriter.Close();
            stream.Close();
        }
    }
}
