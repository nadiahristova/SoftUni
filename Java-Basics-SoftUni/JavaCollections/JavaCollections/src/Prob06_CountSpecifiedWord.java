import java.util.Scanner;

public class Prob06_CountSpecifiedWord {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		String line = input.nextLine();
		String wordToMatch = input.nextLine();
		String[] words = line.trim().split("\\W+");
		int count = 0;
		for (int i = 0; i < words.length; i++) {			
			if (words[i].toLowerCase().equals(wordToMatch.toLowerCase())) { 
				count++;
			}	
		}
		System.out.println(count);		
	}
}
