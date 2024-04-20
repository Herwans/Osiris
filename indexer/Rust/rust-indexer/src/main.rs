use clap::Parser;

use std::fs::{self};
use std::io::{self, Write};
use std::path::PathBuf;

#[derive(Parser)]
struct Cli {
    output_path: std::path::PathBuf,
    starting_point: std::path::PathBuf,
}

fn get_files(source_path: PathBuf) -> Result<Vec<PathBuf>, io::Error> {
    let mut files = Vec::new();

    if let Ok(paths) = fs::read_dir(source_path) {
        for path in paths {
            if let Ok(entry) = path {
                let file_path = entry.path();
                if file_path.is_dir() {
                    if let Ok(mut dir_files) = get_files(file_path) {
                        files.append(&mut dir_files);
                    }
                } else {
                    files.push(file_path);
                }
            }
        }
    } else {
        return Err(io::Error::new(
            io::ErrorKind::Other,
            "Error reading directory",
        ));
    }
    Ok(files)
}

fn main() {
    let args = Cli::parse();

    if let Ok(files) = get_files(args.starting_point) {
        if let Ok(mut file_writer) = fs::File::create(args.output_path) {
            for file in files {
                writeln!(file_writer, "{}", file.display());
            }
        }
    }
}
