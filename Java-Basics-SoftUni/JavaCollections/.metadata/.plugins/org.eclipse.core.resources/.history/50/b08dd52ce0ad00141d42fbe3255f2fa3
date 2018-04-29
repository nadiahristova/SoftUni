package Exam4;

import java.util.Scanner;

public class _03_MovingArrows {
	private static char[][] theMat;

	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		int n = Integer.parseInt(input.nextLine());
		String line = input.nextLine();
		char[][] matrix = new char[n][line.length()];
		int counter = 0;
		for (int colmn = 0; colmn < line.length(); colmn++) {
			matrix[0][colmn] = line.charAt(colmn);
			if (line.charAt(colmn) == '<' || line.charAt(colmn) == '>'
					|| line.charAt(colmn) == 'v' || line.charAt(colmn) == '^') {
				counter++;
			}
		}

		for (int row = 1; row < matrix.length; row++) {
			String roww = input.nextLine();
			for (int colmn = 0; colmn < line.length(); colmn++) {
				matrix[row][colmn] = roww.charAt(colmn);
				if (roww.charAt(colmn) == '<' || roww.charAt(colmn) == '>'
						|| roww.charAt(colmn) == 'v'
						|| roww.charAt(colmn) == '^') {
					counter++;
				}
			}
		}
		//theMat = matrix;

		// boolean isMoving = true;
		int currCounter;
		do {
			char[][] buffMatrix = cloneM(matrix);
			currCounter = 0;
			for (int row = 0; row < matrix.length; row++) {
				for (int colmn = 0; colmn < line.length(); colmn++) {
					char currChar = matrix[row][colmn];
					if (currChar == '<' || currChar == '>' || currChar == 'v'
							|| currChar == '^') {
						switch (currChar) {
						case '<': {
							if ((colmn > 0 && buffMatrix[row][colmn - 1] == 'o')) {
								buffMatrix[row][colmn-1] = '<';
								buffMatrix[row][colmn] = 'o';
							} else currCounter++;								
						}break;
						case '>': {
							if ((colmn < line.length()-1 && buffMatrix[row][colmn + 1] == 'o')) {
								buffMatrix[row][colmn+1] = '>';
								buffMatrix[row][colmn] = 'o';
							} else currCounter++;								
						}break;
						case '^': {
							if ((row > 0 && buffMatrix[row-1][colmn] == 'o')) {
								buffMatrix[row-1][colmn] = '^';
								buffMatrix[row][colmn] = 'o';
							} else currCounter++;								
						}break;
						case 'v': {
							if ((row < matrix.length-1 && buffMatrix[row+1][colmn] == 'o')) {
								buffMatrix[row+1][colmn] = 'v';
								buffMatrix[row][colmn] = 'o';
							} else currCounter++;								
						}break;
						}
					}
				}
			}
			matrix = cloneM(buffMatrix);
		} while (!(currCounter == counter));
		
		for (int i = 0; i < matrix.length; i++) {
			System.out.println(String.valueOf(matrix[i]));
		}
	}

	private static char[][] cloneM(char[][] matrix) {
		char[][] mat = new char[matrix.length][matrix[0].length];
		for (int i = 0; i < matrix.length; i++) {
			for (int j = 0; j < matrix[0].length; j++) {
				mat[i][j] = matrix[i][j]; 
			}
		}		
		return mat;
	}
}
