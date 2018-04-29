import java.util.ArrayList;
import java.util.Scanner;

public class BePositive {
	public static void main(String[] args) {
		@SuppressWarnings("resource")
		Scanner scn = new Scanner(System.in);
		
		int countSequences = scn.nextInt();
		//here we need to add 1 line of code to "absorb" the ENTER after the countOfSequences 
		scn.nextLine(); // - added line of code
				
		for (int i = 0; i < countSequences; i++) {
			String[] input = scn.nextLine().trim().split("\\s+");
//			if (countSequences == 0) {
//				System.out.println("(empty)");
//				return;
//			}
			ArrayList<Integer> numbers = new ArrayList<>();
			
			for (int j = 0; j < input.length; j++) {
				if (!input[j].equals("") ) {
					//int num = Integer.parseInt(input[i]);- here we use the index for 
					//countSequence instead of the index in the input array - in other words
					//we have to change i index with j index
					int num = Integer.parseInt(input[j]); // - modified line of code					
					numbers.add(num);	
				}							
			}
			
			boolean found = false;	

			for (int j = 0; j < numbers.size(); j++) {				
				int currentNum = numbers.get(j);
				
				if (currentNum >= 0) {
					System.out.printf("%d%s", currentNum, j == (numbers.size() - 1) ? "\n" : " ");
					found = true;
				} else {
					if (j < numbers.size()-1) {					
						currentNum += numbers.get(j + 1);					
						
						if (currentNum >= 0) {
							System.out.printf("%d%s", currentNum, j == (numbers.size() - 2) ? "\n" : " ");// if we have (numbers.size() - 2) then we type ENTER
							found = true;
						}	
					}
					j++;// j++ added to skip a turn in the loop
				}
			}
			
			if (!found) {
				System.out.println("(empty)");
			} 			
		}		
	}
}
