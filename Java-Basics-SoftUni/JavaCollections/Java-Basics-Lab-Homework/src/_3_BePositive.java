 //The program passes all tests which were given.
//But I don't know why when an input test is copied in the console 
//the last input line and the first output line are on the same row and
//  the last line from the result appears after the user presses enter.
// I will be thankful if you manage to resolve this issue.
import java.util.ArrayList;
import java.util.Scanner;

public class _3_BePositive {

	public static void main(String[] args) {
		@SuppressWarnings("resource")
		Scanner scn = new Scanner(System.in);
		int num=0;
		int countSequences = scn.nextInt();
		scn.nextLine();
		System.out.println();
		for (int i = 0; i < countSequences; i++) {
			
			String[] input = scn.nextLine().trim().split("\\s+");
			ArrayList<Integer> numbers = new ArrayList<>();

			for (int j = 0; j < input.length; j++) {
				if (!input[j].equals("") ) {
					num = Integer.parseInt(input[j]);
					numbers.add(num);	
				}							
			}
			boolean found = false;

			for (int j = 0; j < numbers.size(); j++) {				
				int currentNum = numbers.get(j);

				if (currentNum >= 0) {
					System.out.printf("%d%s", currentNum, j == numbers.size() - 1 ? "\n" : " ");
					found = true;
				} 
				else {
					if (j+1<=numbers.size()-1) {
					currentNum += numbers.get(j + 1);	
					j++;
					}
					if (currentNum >= 0) {
						System.out.printf("%d%s", currentNum, j == numbers.size() - 1 ? "\n" : " ");
						found = true;
					}				
				}		
			}
			if (!found) {
				System.out.println("(empty)");
			}		
		}		
	}
}
