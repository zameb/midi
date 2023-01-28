using Zameb.ChordFinder;

Console.WriteLine("Hello, World!");
var fileName = @"C:\Users\zameb\Documents\Hotel California_excellent (lyrics).mid";

var manager = new MidiFileManager();
manager.Open(fileName);
var chords = manager.ReadChords();
