import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;

public class Prob8_SumNumbersFromTextFile_2 {
	public static void main(String[] args){
		int sum = 0;
		
		try {
			Scanner fileInput = new Scanner(new File("someFile.txt"));			
			while (fileInput.hasNextInt()) {
				int num = fileInput.nextInt();
				sum += num;
			}
		} catch (FileNotFoundException e) {
			System.out.println("Error.");
		}
		
		System.out.println(sum);
	}

}
