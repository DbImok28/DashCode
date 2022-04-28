﻿using DashCode.Models.DocumentReaders;
using System;

namespace DashCode.Models
{
    public class EditorDocument
    {
        public EditorDocument(string rawDocument, DocumentReader reader)
        {
            if (string.IsNullOrWhiteSpace(rawDocument))
            {
                throw new ArgumentException($"'{nameof(rawDocument)}' cannot be null or whitespace.", nameof(rawDocument));
            }
            File = new ProjectFile
            {
                Content = rawDocument
            };
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }
        public EditorDocument(ProjectFile file, DocumentReader reader)
        {
            Open(file);
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }
        public IConstruction ReadedDocument { get; private set; }
        public ProjectFile File { get; private set; }
        public string RawDocument => File.Content;
        public DocumentReader Reader { get; set; }
        public void Open(ProjectFile file)
        {
            File = file;
            File.ReadContent();
        }
        public void SetText(string document)
        {
            File.Content = document;
        }
        public void Save()
        {
            File.Save();
        }
        public void Read()
        {
            var doc = Reader.Read(this);
            if (doc != null)
                ReadedDocument = doc;
        }
    }
}
