//package BitRotation;

import java.util.Scanner;

public class BitCarousel {

    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        int number = Integer.parseInt(input.nextLine());
        //System.out.println(Integer.toBinaryString(number));
        byte rotations = Byte.parseByte(input.nextLine());

        for (int i = 0; i < rotations; i++) {// added -1
            String direction = input.nextLine();

            if (direction.equals("right")) {//no direct comparison of strings 
                int rightMostBit = number & 1;
                number >>= 1;
                number |= rightMostBit << 5;//6 changed to 5
                //System.out.println(Integer.toBinaryString(number));
            } else if (direction.equals("left")) { //no direct comparison of strings 
                int leftMostBit = (number >> 5) & 1;// 6 changed to 5
                number <<= 1;
                number &= 63;//added to delete every 1 after the 5th place in binary represented number
                number |= leftMostBit;
                //System.out.println(Integer.toBinaryString(number));
            }
        }

        System.out.println(number);
    }
}
