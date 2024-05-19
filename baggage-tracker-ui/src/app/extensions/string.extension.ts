import './string.extension.d';

String.prototype.toTitleCase = function (): string {
	return this.replace(/([A-Z])/g, ' $1').trim();
};
