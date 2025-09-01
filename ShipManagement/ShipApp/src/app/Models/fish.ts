export class Fish {
    fishId: number | undefined;
    fishName: string | undefined;
    constructor();
    constructor(id?: number, name?: string) {
        this.fishId = id ?? 0;
        this.fishName = name ?? undefined;
    }
}
