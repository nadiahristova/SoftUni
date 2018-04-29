//package Substring;

import java.util.Scanner;

public class Substring {

    public static void main(String[] args) {

        Scanner input = new Scanner(System.in);

        String text = input.nextLine();        
        int jump = Integer.parseInt(input.nextLine());        

        char search = 'p';
        boolean hasMatch = false;
        
        for (int i = 0; i < text.length(); i++) {
            if (text.charAt(i)==search) {
                hasMatch = true;

                int endIndex = i+jump+1;//changed from -> int endIndex = jump;

                if (endIndex > text.length()) {
                    endIndex = text.length();
                }

                String matchedString = text.substring(i, endIndex);
                System.out.println(matchedString);
                i += jump;
            }
        }    

        if (!hasMatch) {
            System.out.println("no");
        }
    }
}
