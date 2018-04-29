import java.util.Scanner;

public class Prob02_SequenceOfEqualStrings {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		String line = input.nextLine().trim();
		String[] arrStrings = line.trim().split("\\s+");
			
		for (int i = 0; i < arrStrings.length; i++) {
			int counter = 0;
			String sequence = arrStrings[i] + " ";
			for (int j = i+1; j < arrStrings.length; j++) {
				if (arrStrings[j].equals(arrStrings[i])) {
					sequence += arrStrings[j] + " ";
					counter++;
				} else break;
			}			
			System.out.println(sequence);
			i+=counter;
		}		
	}
}
