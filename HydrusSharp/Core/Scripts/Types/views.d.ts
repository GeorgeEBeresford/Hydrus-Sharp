interface IResultViewModel {

    Value: any;
    Error: string;
}

interface INamedJsonDump {

    DumpType: number;
    DumpName: string;
    Version: number;
    Timestamp: number;
    Dump: string;
}

interface IHashedJsonDump {

    DumpType: number;
    Version: number;
    Dump: string;
    HashedString: string;
}

interface IFileInfo {

    HashId: number;
    Size: number;
    MimeType: number;
    Width: number
    Height: number;
    Duration: number;
    FrameCount: number;
    HasAudio: boolean;
    WordCount: number;
}