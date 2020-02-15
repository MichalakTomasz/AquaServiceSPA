import { Directive, ElementRef, Renderer2, Input, HostListener } from '@angular/core';

@Directive({
  selector: '[clickInfo]'
})
export class ClickInfoDirective {

  constructor(
    private elem: ElementRef,
    private renderer: Renderer2
  ) { }

  @Input()
  text: string;

  //you can use: left, right, top, bottom
  @Input()
  position = 'right';

  private info: any;

  ngOnInit(): void {
    this.info = this.renderer.createElement('div');
    this.info.innerText = this.text;
    this.renderer.setStyle(this.info, 'background', 'black');
    this.renderer.setStyle(this.info, 'position', 'absolute');
    this.renderer.setStyle(this.info, 'color', 'white');
    this.renderer.setStyle(this.info, 'padding', '15px');
    this.renderer.setStyle(this.info, 'border-radius', '8px');
    this.renderer.setStyle(this.info, 'border', '2px solid white');
    this.renderer.setStyle(this.info, 'visibility', 'hidden');
    
    switch (this.position) {
      case 'left': {
        break;
      }
      case 'top': {
        let height = this.elem.nativeElement.offsetHeight * 2;
        this.renderer.setStyle(this.info, 'top', this.elem.nativeElement.offsetTop - height + 'px');
        this.renderer.setStyle(this.info, 'left', this.elem.nativeElement.offsetLeft + 'px');
        break;
      }
      case 'bottom': {
        break;
      }
      default: {
        let height = this.elem.nativeElement.offsetHeight * 1.5;
        this.renderer.setStyle(this.info, 'top', this.elem.nativeElement.offsetTop - height + 'px');
        this.renderer.setStyle(this.info, 'left', this.elem.nativeElement.offsetLeft + this.elem.nativeElement.offsetWidth + 'px');
        break;
      }
    }

    this.renderer.setStyle(this.info, 'z-index', '1');
    const nextElement = this.renderer.nextSibling(this.elem.nativeElement);
    this.renderer.insertBefore(this.elem.nativeElement.parentNode, this.info, nextElement);

  }

  @HostListener('click')
  onClick() {
    if (this.info.style.visibility === 'visible')
      this.info.style.visibility = 'hidden';
    else this.info.style.visibility = 'visible';
  }
}
