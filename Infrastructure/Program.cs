// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var baseDirectory = Directory.GetCurrentDirectory();
var relativePath = Path.Combine(baseDirectory, "../SSO");

// Use GetFullPath to resolve the relative path correctly
var resolvedPath = Path.GetFullPath(relativePath);
Console.WriteLine(resolvedPath);
