import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import java.util.Scanner;

public class Prob05_CountAllWords {
	public static void main(String[] args) throws IOException {		
		Scanner input = new Scanner(System.in);
		String text = input.nextLine();
		String[] words = text.trim().split("\\W+");
		//String[] words = text.trim().split("[^a-zA-z]+");
		System.out.println(words.length);
	}	
}
