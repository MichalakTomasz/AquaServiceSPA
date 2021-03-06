import { Directive, ElementRef, Renderer2, Input, HostListener } from '@angular/core';

@Directive({
  selector: '[popupInfo]'
})
export class PopupInfoDirective {

  constructor(
    private elem: ElementRef,
    private renderer: Renderer2
  ) { }

  @Input()
  text: string;

  @Input()
  position = 'right';

  private info: any;

  @HostListener('click')
  onClick(){
    if (this.info === undefined 
      || this.info.visibility == 'hidden'){
        this.info = this.renderer.createElement('div');
        this.info.innerText = this.text;
        this.renderer.setStyle(this.info, 'background', 'black');
        this.renderer.setStyle(this.info, 'position', 'absolute');
        this.renderer.setStyle(this.info, 'color', 'white');
        this.renderer.setStyle(this.info, 'padding', '15px');
        this.renderer.setStyle(this.info, 'border-radius', '8px');
        this.renderer.setStyle(this.info, 'border', '2px solid white');   
        this.renderer.setStyle(this.info, 'z-index', '1');
        const nextElement = 
        this.renderer.nextSibling(this.elem.nativeElement);
        this.renderer.insertBefore(
          this.elem.nativeElement.parentNode, 
          this.info, nextElement);    
      }
      else this.info.visibility = 'hidden';
  }
}
