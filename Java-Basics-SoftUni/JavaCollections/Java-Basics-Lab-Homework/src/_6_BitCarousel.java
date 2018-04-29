import java.util.Scanner;
public class _6_BitCarousel {
	
	    public static void main(String[] args) {
	        Scanner input = new Scanner(System.in);

	        byte number = Byte.parseByte(input.nextLine());
	        byte rotations = Byte.parseByte(input.nextLine());

	        for (int i = 0; i < rotations; i++) {
	            String direction = input.nextLine();

	            if (direction.equals("right"))  {
	                int rightMostBit = number & 1;
	                number >>= 1;
	                number |= rightMostBit << 5;
	            } else if (direction.equals("left")) {
	            	
	               int leftMostBit = (number >> 5 & 1);
	                number <<= 1;
	                number ^=leftMostBit<<6;
	                number |= leftMostBit;
	            }
	        }
	        System.out.println(number);
	    }
	}
