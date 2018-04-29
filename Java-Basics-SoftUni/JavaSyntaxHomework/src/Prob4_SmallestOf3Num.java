import java.util.Arrays;
import java.util.Scanner;

public class Prob4_SmallestOf3Num {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		
		float[] nums = new float[3];
		for (int i = 0; i < nums.length; i++) {
			System.out.print("Enter a number: ");
			nums[i] = input.nextFloat();			
		}
		Arrays.sort(nums);
		System.out.println("The smallest of the three numbers is " + nums[0]);
	}

}
