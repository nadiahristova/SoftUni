import java.util.*;
public class SymmetricNumbersInRange {
	
	public static boolean isSymmetric(int i){
		if (Integer.toString(i).equals(new StringBuilder(Integer.toString(i)).reverse().toString())){
	        return true;
		}
	    else{
	        return false;
	    }
	}
	
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		int start = input.nextInt();
		int end = input.nextInt();
		for(int i = start ; i<=end ; i++){
			if(isSymmetric(i)==true){
				System.out.print(i + " ");
			}
		}
	}
}
