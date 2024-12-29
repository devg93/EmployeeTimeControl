import { Controller, Get, Post, Body, Patch, Param, Delete } from '@nestjs/common';
import { GetdataService } from './getdata.service';
import { CreateGetdatumDto } from './dto/create-getdatum.dto';
import { UpdateGetdatumDto } from './dto/update-getdatum.dto';

@Controller('getdata')
export class GetdataController {
  constructor(private readonly getdataService: GetdataService) {}

  @Post()
  create(@Body() createGetdatumDto: CreateGetdatumDto) {
    return this.getdataService.create(createGetdatumDto);
  }

  @Get()
  findAll() {
    return this.getdataService.findAll();
  }

  @Get(':id')
  findOne(@Param('id') id: string) {
    return this.getdataService.findOne(+id);
  }

  @Patch(':id')
  update(@Param('id') id: string, @Body() updateGetdatumDto: UpdateGetdatumDto) {
    return this.getdataService.update(+id, updateGetdatumDto);
  }

  @Delete(':id')
  remove(@Param('id') id: string) {
    return this.getdataService.remove(+id);
  }
}
