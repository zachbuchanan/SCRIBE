export class Section {
    constructor(id: number, value: string, isselected: boolean){
        this.Id = id;
        this.Value = value;
        this.IsSelected = isselected;
    }
    Id: number;
    Value: string = '';
    IsSelected: boolean;
}
