package com.flipper.enforce;

import com.flipper.enforce.dayz.EnforceParser;

import java.nio.file.Paths;

public class EnforceTools {
    public static void main(String[] args) throws Exception{
        EnforceParser parser = new EnforceParser(Paths.get("C:\\Users\\dev\\Downloads\\HitDirectionBase.c").toFile());
    }
}
