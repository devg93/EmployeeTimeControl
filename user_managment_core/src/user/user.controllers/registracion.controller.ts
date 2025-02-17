import { Controller, Get, Post, Body, Patch, Param, Delete } from '@nestjs/common';

import { CreateRegistracionDto } from '../dto/create-registracion.dto';
import { UpdateRegistracionDto } from '../dto/update-registracion.dto';
import { RegistracionService } from '../user.services/registracion.service';
import { ApiOperation } from '@nestjs/swagger';


@Controller('registracion')
export class RegistracionController {
  constructor(private readonly registracionService: RegistracionService) {}

  @Post('register')
  @ApiOperation({ summary: 'Register a new user' })
  async register(@Body() body: CreateRegistracionDto) {
    return this.registracionService.register(body);
  }
  @Get()
  findAll() {
    return this.registracionService.findAll();
  }

  @Get(':id')
  findOne(@Param('id') id: string) {
    return this.registracionService.findOneUser(+id);
  }

  @Patch(':id')
  update(@Param('id') id: string, @Body() updateRegistracionDto: UpdateRegistracionDto) {
    return this.registracionService.updateUser(+id, updateRegistracionDto);
  }

  @Delete(':id')
  remove(@Param('id') id: string) {
    return this.registracionService.remove(+id);
  }
}
