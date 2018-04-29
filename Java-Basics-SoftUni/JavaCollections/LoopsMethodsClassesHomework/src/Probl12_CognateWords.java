import java.util.Scanner;
import java.util.Set;
import java.util.TreeSet;

public class Probl12_CognateWords {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		String line = input.nextLine();
		String[] words = line.trim().split("[^a-zA-Z]+");	
		boolean areCognative = false;		
		Set<String> listAnswers= new TreeSet<>();
		
		for (int i = 0; i < words.length; i++) {			
			for (int j = 0; j < words.length; j++) {
				for (int j2 = 0; j2 < words.length; j2++) {
					if (i!=j && j!=j2 && j2!=i) {
						String a = words[i];
						String b = words[j];
						String c = words[j2];
						if ((a+b).equals(c)) {
							listAnswers.add(a+"|"+b +"="+c);
							areCognative = true;
						}
					}
				}
			}
		}		
		
		for(String str:listAnswers){
			System.out.println(str);
		}
		if (!areCognative) {
			System.out.println("No");
		}
	}
}
