#include <iostream>
#include <filesystem>
#include <string>
#include <fstream>

int main(int argc, char *argv[])
{
    if (sizeof(*argv) < 2)
    {
        std::cout << "Missing folder path" << std::endl;
        return 1;
    }
    std::cout << "Indexer C++" << std::endl;

    int countFile = 0;
    int countDir = 0;

    std::ofstream outputFile;
    outputFile.open("output/cpp/index.txt");
    for (const auto &file : std::filesystem::recursive_directory_iterator(argv[1]))
    {
        outputFile << file.path() << "\n";
    }
    outputFile.close();

    return 0;
}