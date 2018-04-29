import java.util.Scanner;


public class LongestOddEvenSeq {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		String line  = input.nextLine().trim().replace(" ", "");
		String[] nums = line.split("[()]+");
		int counter = 0, maxSequ = 0;
		boolean odd = Integer.parseInt(nums[1]) %2 == 0;
		for (int i = 2; i < nums.length; i++) {
			boolean isOdd = Integer.parseInt(nums[i]) %2 == 0;
			if (odd != isOdd || Integer.parseInt(nums[i]) == 0) {
				counter++;
			} else {				
				odd = isOdd;
				counter =1;
			}
			odd = !odd;
			if (maxSequ < counter) {
				maxSequ = counter;
			}
		}
		System.out.println(maxSequ);
	}
}
