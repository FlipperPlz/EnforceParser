package com.flipper.enforce;

import com.flipper.enforce.dayz.EnforceParser;

import java.nio.file.Paths;

public class EnforceTools {
    public static void main(String[] args) throws Exception{
        EnforceParser.parseEnforceFile(Paths.get(args[0]).toFile());
    }
}
