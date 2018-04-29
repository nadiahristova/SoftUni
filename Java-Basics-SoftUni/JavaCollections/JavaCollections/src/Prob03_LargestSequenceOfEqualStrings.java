import java.util.Scanner;


public class Prob03_LargestSequenceOfEqualStrings {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		String line = input.nextLine().trim();
		String[] arrStr = line.split("\\s+");
		int maxCounter = 0;
		String longestSec = null;
		
		for (int i = 0; i < arrStr.length; i++) {
			int counter = 1;			
			String sequence = arrStr[i] + " ";
			for (int j = i+1; j < arrStr.length; j++) {
				if (arrStr[i].equals(arrStr[j])) {
					counter++;
					sequence += arrStr[j] + " ";
				} else break;
			}
			i+=counter-1;
			if (counter > maxCounter) {
				longestSec = sequence;
				maxCounter = counter;
			}
		}
		System.out.println(longestSec);
	}
}
