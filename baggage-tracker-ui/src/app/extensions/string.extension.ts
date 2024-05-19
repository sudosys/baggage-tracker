export const toTitleCase = (input: string): string =>
	input.replace(/([A-Z])/g, ' $1').trim();
