import { Controller, Get, Post, Body, Patch, Param, Delete } from '@nestjs/common';

import { CreateRegistracionDto } from '../dto/create-registracion.dto';
import { UpdateRegistracionDto } from '../dto/update-registracion.dto';
import { RegistracionService } from '../user.services/registracion.service';


@Controller('registracion')
export class RegistracionController {
  constructor(private readonly registracionService: RegistracionService) {}

  @Post()
  create(@Body() createRegistracionDto: CreateRegistracionDto) {
    return this.registracionService.create(createRegistracionDto);
  }

  @Get()
  findAll() {
    return this.registracionService.findAll();
  }

  @Get(':id')
  findOne(@Param('id') id: string) {
    return this.registracionService.findOne(+id);
  }

  @Patch(':id')
  update(@Param('id') id: string, @Body() updateRegistracionDto: UpdateRegistracionDto) {
    return this.registracionService.update(+id, updateRegistracionDto);
  }

  @Delete(':id')
  remove(@Param('id') id: string) {
    return this.registracionService.remove(+id);
  }
}
