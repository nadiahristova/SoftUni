import java.util.Scanner;

public class Prob2_Generate3LetterWords {
	public static void main(String[] args) {
		Scanner scn = new Scanner(System.in);
		String iniInput = scn.nextLine();			
 		
		for (int i = 0; i < iniInput.length(); i++) {			
			for (int j = 0; j < iniInput.length(); j++) {
				for (int k = 0; k < iniInput.length(); k++) {
					String answer = "" + iniInput.charAt(i)+iniInput.charAt(j)+iniInput.charAt(k);
					System.out.print(answer + " ");									
				}
			}
		}
		System.out.println();
	}
}
